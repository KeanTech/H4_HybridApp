﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Collections.Generic;
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
		public ITable<BoardTask>    BoardTasks    { get { return this.GetTable<BoardTask>(); } }
		public ITable<Priority>     Priorities    { get { return this.GetTable<Priority>(); } }
		public ITable<ProjectBoard> ProjectBoards { get { return this.GetTable<ProjectBoard>(); } }
		public ITable<Section>      Sections      { get { return this.GetTable<Section>(); } }
		public ITable<Priority>       Status        { get { return this.GetTable<Priority>(); } }

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

	[Table(Schema="dbo", Name="BoardTask")]
	public partial class BoardTask
	{
		[PrimaryKey, Identity] public int    Id         { get; set; } // int
		[Column,     Nullable] public string Name       { get; set; } // varchar(25)
		[Column,     Nullable] public string Text       { get; set; } // varchar(max)
		[Column,     Nullable] public int?   PriorityId { get; set; } // int
		[Column,     Nullable] public int?   SectionId  { get; set; } // int
		[Column,     Nullable] public int?   StatusId   { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__BoardTask__Prior__2D27B809 (dbo.Priority)
		/// </summary>
		[Association(ThisKey="PriorityId", OtherKey="Id", CanBeNull=true)]
		public Priority Priority { get; set; }

		/// <summary>
		/// FK__BoardTask__Secti__2E1BDC42 (dbo.Section)
		/// </summary>
		[Association(ThisKey="SectionId", OtherKey="Id", CanBeNull=true)]
		public Section Section { get; set; }

		/// <summary>
		/// FK__BoardTask__Statu__2F10007B (dbo.Priority)
		/// </summary>
		[Association(ThisKey="StatusId", OtherKey="Id", CanBeNull=true)]
		public Priority Status { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Priority")]
	public partial class Priority
	{
		[PrimaryKey, Identity] public int    Id   { get; set; } // int
		[Column,     Nullable] public string Name { get; set; } // varchar(25)

		#region Associations

		/// <summary>
		/// FK__BoardTask__Prior__2D27B809_BackReference (dbo.BoardTask)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="PriorityId", CanBeNull=true)]
		public IEnumerable<BoardTask> BoardTaskPriority { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="ProjectBoard")]
	public partial class ProjectBoard
	{
		[PrimaryKey, Identity] public int    Id   { get; set; } // int
		[Column,     Nullable] public string Name { get; set; } // varchar(30)

		#region Associations

		/// <summary>
		/// FK__Section__Project__2A4B4B5E_BackReference (dbo.Section)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ProjectBoardId", CanBeNull=true)]
		public IEnumerable<Section> ProjectSections { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Section")]
	public partial class Section
	{
		[PrimaryKey, Identity] public int    Id             { get; set; } // int
		[Column,     Nullable] public string Name           { get; set; } // varchar(25)
		[Column,     Nullable] public int?   ProjectBoardId { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__BoardTask__Secti__2E1BDC42_BackReference (dbo.BoardTask)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="SectionId", CanBeNull=true)]
		public IEnumerable<BoardTask> SectionBoardTasks { get; set; }

		/// <summary>
		/// FK__Section__Project__2A4B4B5E (dbo.ProjectBoard)
		/// </summary>
		[Association(ThisKey="ProjectBoardId", OtherKey="Id", CanBeNull=true)]
		public ProjectBoard ProjectBoard { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static BoardTask Find(this ITable<BoardTask> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Priority Find(this ITable<Priority> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ProjectBoard Find(this ITable<ProjectBoard> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Section Find(this ITable<Section> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}
