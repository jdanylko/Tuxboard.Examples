﻿using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class TuxboardUserConfiguration: IEntityTypeConfiguration<TuxboardUser>
{
    public void Configure(EntityTypeBuilder<TuxboardUser> builder)
    {
        builder.ToTable(nameof(TuxboardUser));
    }
}