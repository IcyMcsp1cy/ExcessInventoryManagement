USE [ExcessInventoryManagement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MarkdownPlan](
	[MarkdownPlanId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[PlanName] [varchar](25) NOT NULL,
	[StartDate] [Date] NULL,
	[EndDate] [Date] NULL,
	[InitialPriceReduction] [numeric](18, 0) NULL,
	[MidwayPriceReduction] [numeric](18, 0) NULL,
	[FinalPriceReduction] [numeric](18, 0) NULL,
 CONSTRAINT [PK_MarkdownPlan] PRIMARY KEY CLUSTERED
(
	[MarkdownPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MarkdownPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_MarkdownPlan_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO