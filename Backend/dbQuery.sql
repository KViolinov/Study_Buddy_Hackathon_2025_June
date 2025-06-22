-- WARNING: This schema is for context only and is not meant to be run.
-- Table order and constraints may not be valid for execution.

CREATE TABLE public.inputs (
  input_id integer NOT NULL DEFAULT nextval('inputs_inputid_seq'::regclass),
  user_id integer NOT NULL,
  time_of_sending timestamp without time zone NOT NULL,
  type character varying NOT NULL,
  input_source_type character varying NOT NULL,
  input_text text NOT NULL,
  CONSTRAINT inputs_pkey PRIMARY KEY (input_id),
  CONSTRAINT fk_inputs_users FOREIGN KEY (user_id) REFERENCES public.users(user_id)
);
CREATE TABLE public.outputs (
  output_id integer NOT NULL DEFAULT nextval('outputs_output_id_seq'::regclass),
  user_id integer NOT NULL,
  time_of_sending timestamp without time zone NOT NULL,
  type character varying NOT NULL,
  output_text json NOT NULL,
  input_id integer NOT NULL,
  CONSTRAINT outputs_pkey PRIMARY KEY (output_id),
  CONSTRAINT fk_outputs_users FOREIGN KEY (user_id) REFERENCES public.users(user_id),
  CONSTRAINT fk_outputs_inputs FOREIGN KEY (input_id) REFERENCES public.inputs(input_id)
);
CREATE TABLE public.users (
  user_id integer NOT NULL DEFAULT nextval('users_user_id_seq'::regclass),
  user_email character varying NOT NULL UNIQUE,
  username character varying NOT NULL,
  password character varying NOT NULL,
  CONSTRAINT users_pkey PRIMARY KEY (user_id)
);