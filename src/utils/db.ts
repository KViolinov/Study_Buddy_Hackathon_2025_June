import { createClient } from "@supabase/supabase-js";

const supabaseUrl = "https://qjbwcxqxtgftawrqxuec.supabase.co";
const supabaseKey =
  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InFqYndjeHF4dGdmdGF3cnF4dWVjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA1MTg5NDcsImV4cCI6MjA2NjA5NDk0N30.DWwk1lldHiuZwqocTFMQD3w-cNt8geJaeq1ZeRT84Go";
export const supabase = createClient(supabaseUrl, supabaseKey);
