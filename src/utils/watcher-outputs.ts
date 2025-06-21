import { supabase } from "./db";

export async function pollForOutput(inputId: number) {
  const { data, error } = await supabase
    .from("outputs")
    .select("*")
    .eq("input_id", inputId)
    .limit(1);

  if (error) throw new Error(error.message);
  if (!data || data.length === 0) return null;

  return data[0];
}
