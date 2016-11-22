CREATE TABLE [dbo].[Todo] (
	[TodoId]        UNIQUEIDENTIFIER PRIMARY KEY NONCLUSTERED,
	[ClusteringKey] INT NOT NULL IDENTITY(1,1),
	[Title]         NVARCHAR(255) NOT NULL,
	[Completed]     BIT NOT NULL
)

CREATE CLUSTERED INDEX Todo_Clustered on [dbo].[Todo] ([ClusteringKey])