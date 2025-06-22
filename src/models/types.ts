export interface InputData {
  user_id: string;
  time_of_sending: string;
  type: string;
  input_source_type: string;
  input_text: string;
}

export interface OutputData {
  output_id: string;
  input_id: string;
  output_text: string;
  created_at: string;
}
