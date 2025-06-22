import { pollForOutput } from "../utils/watcher-outputs";
import { OutputData } from "../models/types";

export abstract class OutputManager {
  static async pollOutput(inputId: number): Promise<OutputData | null> {
    return await pollForOutput(inputId);
  }
}
