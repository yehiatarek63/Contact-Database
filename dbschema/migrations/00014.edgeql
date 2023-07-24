CREATE MIGRATION m13g7wb4uzsyrm22actsbozn7ynu3q4apyc57zb2mc7ptlk6ccvdla
    ONTO m1khajwoozsyxehkii2py2rdd2xl4lbmjax4got2lcuxahccp4oq7q
{
  CREATE SCALAR TYPE default::Roles EXTENDING enum<Admin, User>;
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY roles: default::Roles {
          SET REQUIRED USING (<default::Roles>{'User'});
      };
  };
};
