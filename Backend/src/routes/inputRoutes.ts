import { Router } from "express";
import { InputManager } from "../controllers/inputController";
import { OutputManager } from "../controllers/outputController";
import { InputData } from "../models/types";

const router = Router();

router.post("/inputs", async (req, res) => {
  const { userId, type, inputSourceType, inputText } = req.body;

  const inputObject: InputData = {
    user_id: userId,
    time_of_sending: new Date().toISOString(),
    type,
    input_source_type: inputSourceType,
    input_text: inputText,
  };

  try {
    const inserted = await InputManager.insertInput(inputObject);
    const inputId = inserted.input_id;

    const interval = setInterval(async () => {
      const output = await OutputManager.pollOutput(inputId);

      if (output) {
        clearInterval(interval);
        return res
          .status(200)
          .json({ message: "Output received", data: output });
      }
    }, 5000);
  } catch (error) {
    console.error("Error inserting input:", error);
    let errorResponse: { error: string; reason?: string } = {
      error: "Failed to insert input",
    };
    if (
      error &&
      typeof error === "object" &&
      "message" in error &&
      typeof error.message === "string"
    ) {
      errorResponse.reason = error.message;
    }
    res.status(400).json(errorResponse);
  }
});

export default router;
