# from supabase import create_client, Client
#
# # url = os.getenv("SUPABASE_URL")
# # key = os.getenv("SUPABASE_KEY")
#
# url = "https://qjbwcxqxtgftawrqxuec.supabase.co"
# key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InFqYndjeHF4dGdmdGF3cnF4dWVjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA1MTg5NDcsImV4cCI6MjA2NjA5NDk0N30.DWwk1lldHiuZwqocTFMQD3w-cNt8geJaeq1ZeRT84Go"
#
# supabase: Client = create_client(url, key)
#
# # Define the new record as a dictionary
# new_input = {
#     "user_id": "1",
#     "time_of_sending": "2023-10-01T12:00:00Z",
#     "type": "summary",
#     "input_source_type": "normal text",
#     "input_text": "This is a sample input text for testing purposes.",
# }
#
# # Insert into the Inputs table
# response = supabase.table("inputs").insert(new_input).execute()
#
# print("Inserted:", response.data)






import time

from supabase import create_client, Client

# url = os.getenv("SUPABASE_URL")
# key = os.getenv("SUPABASE_KEY")

url = "https://qjbwcxqxtgftawrqxuec.supabase.co"
key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InFqYndjeHF4dGdmdGF3cnF4dWVjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA1MTg5NDcsImV4cCI6MjA2NjA5NDk0N30.DWwk1lldHiuZwqocTFMQD3w-cNt8geJaeq1ZeRT84Go"

supabase: Client = create_client(url, key)

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

    # Extract and print the single latest row
    if response.data:
        latest = response.data[0]
        print("Latest entry:", latest)
    else:
        print("No entries found.")

    user_id = latest["user_id"]
    time_of_sending = latest["time_of_sending"]
    input_type = latest["type"]
    input_source_type = latest["input_source_type"]
    input_text = latest["input_text"]

    print("sleeping for 15 seconds to avoid rate limiting")
    time.sleep(15)
