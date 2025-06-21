import express from "express";
import { supabase } from "./utils/db";
import { pollOutputs } from "./utils/watcher-outputs";

const app = express();
const PORT = process.env.PORT || 3000;

app.use(express.json());

// Примерен POST маршрут
app.post("/api/inputs", async (req, res) => {
  const { userId, type, inputSourceType, inputText } = req.body;

  // Object processing
  const inputObject = {
    user_id: userId,
    time_of_sending: new Date(),
    type,
    input_source_type: inputSourceType,
    input_text: inputText,
  };

  console.log("Received input object:", inputObject);
  const { data, error } = await supabase.from("inputs").insert([
    {
      user_id: 1,
      time_of_sending: new Date().toISOString(),
      type: "summary",
      input_source_type: "normal text",
      input_text: "Това е тестов текст",
    },
  ]);

  if (error) {
    console.error("Error inserting data:", error);
    throw new Error(error.message);
  } else {
    res.status(201).json({ message: "Input created successfully", data });
  }
});

app.get("/api/output/:input_id", async (req, res) => {
  const inputId = parseInt(req.params.input_id);
  const { data, error } = await supabase
    .from("outputs")
    .select("*")
    .eq("input_id", inputId)
    .limit(1);

  if (error) {
    console.error("Supabase error:", error);
    res.status(500).json({ error: "Database error" });
  } else if (!data || data.length === 0) {
    res.status(404).json({ error: "Output not found" });
  } else {
    res.status(200).json(data![0]);
  }
});

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});
