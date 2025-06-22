import { supabase } from "../utils/db";
import { InputData } from "../models/types";

export abstract class InputManager {
  static async insertInput(input: InputData) {
    const { data, error } = await supabase
      .from("inputs")
      .insert([input])
      .select();
    if (error) throw error;
    return data?.[0];
  }
}
