CREATE MIGRATION m1mddehctuxxgfqr22igqsz2nskxsga74dkzzg23ddm5dftnjonuwa
    ONTO m1xufuqymwfxsjkvfbxsxt5aaidjhlz63zz4nx3qrqvwaz4stv4bvq
{
  CREATE SCALAR TYPE default::State EXTENDING enum<Mr, Mrs, Ms, Dr, Prof>;
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY date_of_birth: std::datetime;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY title: default::State;
  };
};
