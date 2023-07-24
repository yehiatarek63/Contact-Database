CREATE MIGRATION m1ho2xfdgehkxf3fig2ovcly5zneybkbqvgntxux2o4g24rv7sq56a
    ONTO m17vgxr2plwm2ks2nwb7wtzi75rmk4ooopm5ogk2bvpmtbfbik676a
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY password: std::str {
          SET REQUIRED USING (<std::str>{'1234'});
      };
      CREATE REQUIRED PROPERTY username: std::str {
          SET REQUIRED USING (<std::str>{(.first_name ++ .last_name)});
      };
  };
};
