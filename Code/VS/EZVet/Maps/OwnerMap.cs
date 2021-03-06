﻿using Domain;

namespace Maps
{
    public class OwnerMap : EntityMap<Owner>
    {
        public OwnerMap()
        {
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Password);
            Map(x => x.Username);
            Map(x => x.BirthDate);
            Map(x => x.Email);

            References(x => x.Address);

            HasMany(x => x.Animals);
        }
    }
}
