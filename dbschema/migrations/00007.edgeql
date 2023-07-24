CREATE MIGRATION m1ssdhklpp63xsctlxxu6rb2qbe3tnizrpyy2q33up3hs3or2nstkq
    ONTO m1ho2xfdgehkxf3fig2ovcly5zneybkbqvgntxux2o4g24rv7sq56a
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY salt: std::bytes {
          SET REQUIRED USING (<std::bytes>{b'Hello, world'});
      };
  };
};
