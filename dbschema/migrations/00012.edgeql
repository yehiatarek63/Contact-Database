CREATE MIGRATION m1jibmoraxub4qf3mf7zgho3f4v6t4jcgna55kncos6f6kfhmetdvq
    ONTO m1b27z4loiduvbwziyuwdmql62v7uqsriniklgbgra5hknvqooul4q
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
      CREATE REQUIRED PROPERTY salt: std::bytes;
      CREATE REQUIRED PROPERTY title: default::State;
      CREATE REQUIRED PROPERTY username: std::str;
  };
};
