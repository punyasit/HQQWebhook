SELECT * FROM hqq_cp_product_statistic cpps
WHERE cpps.created_on IN (SELECT max(cpps.created_on) FROM hqq_cp_product_statistic cpps2 WHERE cpps.product_id=cpps2.product_id)
AND sale_history < 1000
ORDER BY stock_movement ASC
LIMIT 20