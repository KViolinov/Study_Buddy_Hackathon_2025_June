import express from "express";
import { supabase } from "./utils/db";
import { pollForOutput } from "./utils/watcher-outputs";

const app = express();
const PORT = process.env.PORT || 3000;

app.use(express.json());

app.post("/api/inputs", async (req, res) => {
  const { userId, type, inputSourceType, inputText } = req.body;

  // Object processing
  const inputObject = {
    user_id: userId,
    time_of_sending: new Date().toISOString(),
    type,
    input_source_type: inputSourceType,
    input_text: inputText,
  };

  console.log("Received input object:", inputObject);
  const { data, error } = await supabase
    .from("inputs")
    .insert([inputObject])
    .select();

  const inputId = data![0].input_id;

  const interval = setInterval(async () => {
    const output = await pollForOutput(inputId);

    if (output) {
      clearInterval(interval);
      console.log("Output received:", output);
      return res.status(200).json({ message: "Output received", data: output });
    } else {
      console.log("No output yet, polling again...");
    }
  }, 5000);
});

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});
