CREATE MIGRATION m17vgxr2plwm2ks2nwb7wtzi75rmk4ooopm5ogk2bvpmtbfbik676a
    ONTO m1mddehctuxxgfqr22igqsz2nskxsga74dkzzg23ddm5dftnjonuwa
{
  ALTER SCALAR TYPE default::State EXTENDING enum<Mr, Mrs, Miss, Dr, Prof>;
};
