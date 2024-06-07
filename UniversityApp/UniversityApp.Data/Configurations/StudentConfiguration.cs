﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityApi.Data.Entities;

namespace UniversityApi.Data.Configurations
{
	public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(35);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired(true);
            builder.HasOne(x => x.Group).WithMany(g => g.Students).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

