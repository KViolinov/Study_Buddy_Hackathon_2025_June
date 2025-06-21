import express from "express";
import { supabase } from "./utils/db";
import { pollOutputs } from "./utils/watcher-outputs";

const app = express();
const PORT = process.env.PORT || 3000;

app.use(express.json());
setInterval(() => {
  pollOutputs();
}, 5000);

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
      user_id: 1, // <- замени с реално ID от Users таблицата
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

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});
