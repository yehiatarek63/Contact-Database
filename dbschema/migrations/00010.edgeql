CREATE MIGRATION m1hbsbmvyqxruz2fqprq6ogrh4qdfmnlkmdhpkti7sn7vt6njiv6vq
    ONTO m1nhrsunxw6ljw234v34e2z5pudk7e4aqugo6hzmwbzxgaub4qhxcq
{
  CREATE SCALAR TYPE default::State EXTENDING enum<Mr, Mrs, Miss, Dr, Prof>;
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY date_of_birth: std::datetime;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY password: std::str;
      CREATE REQUIRED PROPERTY salt: std::str;
      CREATE REQUIRED PROPERTY title: default::State;
      CREATE REQUIRED PROPERTY username: std::str;
  };
};
