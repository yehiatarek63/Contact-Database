CREATE MIGRATION m1w67s2cuafighy72qu7vjjkixy6zklknbvh3pfs6kkvmumrs5uokq
    ONTO initial
{
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY date_of_birth: cal::local_date;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY title: std::str;
  };
};
