import { supabase } from "./db";
import axios from "axios";

let lastSeenId = 0;

export async function pollOutputs() {
  const { data, error } = await supabase
    .from("outputs")
    .select("*")
    .gt("output_id", lastSeenId) // само новите
    .order("output_id", { ascending: true });

  if (error) {
    console.error("Error fetching outputs:", error);
    return;
  }

  if (data && data.length > 0) {
    for (const output of data) {
      try {
        // await axios.post("https://your-webhook-url.com", output);
        console.log("Sent output:", output);
      } catch (err) {
        console.error("Failed to send output:", err);
      }

      // обнови lastSeenId
      lastSeenId = Math.max(lastSeenId, output.output_id);
    }
  }
}
