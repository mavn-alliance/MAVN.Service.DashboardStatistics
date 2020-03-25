-- At some point(for example incorect data clearance after customer deletion) there might be difference between statistics and activities
-- This causes some weird percentage numbers in the backoffice
-- This script fixes that

DELETE FROM [dashboard_statistic].[customer_activities]
WHERE [customer_id] IN (
	SELECT DISTINCT [dashboard_statistic].[customer_activities].[customer_id]
	FROM [dashboard_statistic].[customer_activities]
		FULL JOIN [dashboard_statistic].[customer_statistics] ON 
			([dashboard_statistic].[customer_activities].[customer_id] = [dashboard_statistic].[customer_statistics].[customer_id])
	WHERE [dashboard_statistic].[customer_statistics].[customer_id] IS NULL
	)