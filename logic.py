import time
import google.generativeai as genai
from datetime import datetime, timezone
import os
import json

from supabase import create_client, Client

from dotenv import load_dotenv

load_dotenv()

url = os.getenv("SUPABASE_URL")
key = os.getenv("SUPABASE_KEY")

supabase: Client = create_client(url, key)

# Gemini Setup
os.environ["GEMINI_API_KEY"] = os.getenv("GEMINI_API_KEY")
genai.configure(api_key=os.environ["GEMINI_API_KEY"])
model = genai.GenerativeModel(model_name="gemini-1.5-flash")

system_instruction = (
    "You are a study buddy app, i give you full text/lesson about something and you create a quizes, "
    "flash cards and/or summaries based on the information i give you"
)

chat = model.start_chat(history=[{"role": "user", "parts": [system_instruction]}])

last_time_of_sending = None

while True:
    # Fetch the latest entry based on time_of_sending
    response = (
        supabase
        .table("inputs")
        .select("*")
        .order("time_of_sending", desc=True)
        .limit(1)
        .execute()
    )

    if response.data:
        latest = response.data[0]
        if latest["time_of_sending"] == last_time_of_sending:
            print("No new entry. Sleeping...")
            time.sleep(15)
            continue  # Skip processing if not new

        last_time_of_sending = latest["time_of_sending"]
        print("Latest entry:", latest)
    else:
        print("No entries found.")
        time.sleep(15)
        continue

    user_id = latest["user_id"]
    input_id = latest["input_id"]
    time_of_sending = latest["time_of_sending"]
    input_type = latest["type"]
    input_source_type = latest["input_source_type"]
    input_text = latest["input_text"]

    match input_source_type:
        case "normal text":
            user_input = input_text

    match input_type:
        case "quiz":
            # recall = False
            initial_command = ("Create a quiz in JSON format: following"
                               "this strict model format-> your json has to have similar format like this one about quiz:"
                               "public class Quiz {"
                               "public string Question { get; set; }"
                               "public string CorrectAnswer { get; set; }"
                               "public List<string> Options { get; set; }")

        case "flashcards":  # 🟢 fixed
            initial_command = (
                "Create flashcards in JSON format following this strict model format "
                "-> your JSON has to match the structure of the following C# classes: "
                "public class FlashcardsCollection { "
                "[JsonPropertyName(\"flashcards\")] "
                "public List<FlashcardItem> Flashcards { get; set; } "
                "} "
                "public class FlashcardItem { "
                "[JsonPropertyName(\"question\")] "
                "public string Question { get; set; } "
                "[JsonPropertyName(\"answer\")] "
                "public string Answer { get; set; } "
                "} "
                "Make sure to return valid and parseable JSON." )

        case "summary":
            # recall = False
            initial_command = ("Create a summary of the following text/youtube link in JSON format:"
                               "The returned json should be in this format:"
                               "public class Summary {"

                               "public string summary { get; set; }"
                               "public string[] keywords { get; set; } in JSON format:"
                               "The returned json should be in this format:"
                               "public class Summary {"
                               "public string summary { get; set; }"
                               "public string[] keywords { get; set; }")

    send_to_model = initial_command + user_input

    result = chat.send_message({"parts": [send_to_model]})
    response_text = result.text
    print(f"Quiz bot: {response_text}")

    print("Inserting into the database...")

    now_iso = datetime.now(timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ")

    # Define the new record as a dictionary
    new_output = {
        "user_id": user_id,
        "time_of_sending": now_iso,
        "type": input_type,
        "output_text": response_text,
        'input_id': input_id,
    }

    # Insert into the Inputs table
    response = supabase.table("outputs").insert(new_output).execute()

    print("Inserted:", response.data)

    print("sleeping for 15 seconds to avoid rate limiting")
    time.sleep(15)