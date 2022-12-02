﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace BlazorBoard_Api.DataAccess
{
	/// <summary>
	/// Database       : BlazorBoard
	/// Data Source    : localhost
	/// Server Version : 15.00.2000
	/// </summary>
	public partial class BlazorBoardDB : LinqToDB.Data.DataConnection
	{
		public ITable<Section>     Sections     { get { return this.GetTable<Section>(); } }
		public ITable<SectionTask> SectionTasks { get { return this.GetTable<SectionTask>(); } }
		public ITable<User>        Users        { get { return this.GetTable<User>(); } }

		public BlazorBoardDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public BlazorBoardDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public BlazorBoardDB(LinqToDBConnectionOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public BlazorBoardDB(LinqToDBConnectionOptions<BlazorBoardDB> options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="Section")]
	public partial class Section
	{
		[PrimaryKey, Identity] public int    Id          { get; set; } // int
		[Column,     Nullable] public string Status      { get; set; } // varchar(25)
		[Column,     Nullable] public string ProjectName { get; set; } // varchar(50)
	}

	[Table(Schema="dbo", Name="SectionTask")]
	public partial class SectionTask
	{
		[PrimaryKey, Identity] public int    Id        { get; set; } // int
		[Column,     Nullable] public string Status    { get; set; } // varchar(25)
		[Column,     Nullable] public string Text      { get; set; } // varchar(max)
		[Column,     Nullable] public int?   UserId    { get; set; } // int
		[Column,     Nullable] public int?   SectionId { get; set; } // int
	}

	[Table(Schema="dbo", Name="User")]
	public partial class User
	{
		[PrimaryKey, Identity] public int    Id      { get; set; } // int
		[Column,     Nullable] public string Name    { get; set; } // varchar(25)
		[Column,     Nullable] public string Contact { get; set; } // varchar(max)
		[Column,     Nullable] public string Info    { get; set; } // varchar(max)
		[Column,     Nullable] public string Sauce   { get; set; } // varchar(max)
	}

	public static partial class TableExtensions
	{
        public static Section Find(this ITable<Section> table, int Id)
        {
            return table.FirstOrDefault(t =>
                t.Id == Id);
        }

        public static Section FindBySectionStatus(this ITable<Section> table, string status)
        {
            return table.FirstOrDefault(x => x.Status == status);
        }

        public static SectionTask Find(this ITable<SectionTask> table, int Id)
        {
            return table.FirstOrDefault(t =>
                t.Id == Id);
        }

        public static SectionTask FindBySectionStatus(this ITable<SectionTask> table, string status)
        {
            return table.FirstOrDefault(x => x.Status == status);
        }

        public static User Find(this ITable<User> table, int Id)
        {
            return table.FirstOrDefault(t =>
                t.Id == Id);
        }
    }
}
