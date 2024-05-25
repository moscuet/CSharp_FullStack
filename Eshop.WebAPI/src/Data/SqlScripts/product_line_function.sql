DROP FUNCTION IF EXISTS get_product_lines( p_limit integer, p_starting_after integer, p_sort_by text, p_sort_order text, p_search_key text, p_category_name text );
CREATE OR REPLACE FUNCTION get_product_lines(
    p_limit integer,
    p_starting_after integer,
    p_sort_by text,
    p_sort_order text,
    p_search_key text,
    p_category_name text)
    RETURNS TABLE(
        id uuid, 
        title character varying, 
        description character varying, 
        image_url character varying, 
        price decimal(18,2), 
        category_id uuid, 
        created_at timestamp with time zone, 
        updated_at timestamp with time zone
    ) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
DECLARE
    query TEXT;
BEGIN
    query := 'SELECT pl.id, pl.title, pl.description, pl.image_url, pl.price, pl.category_id, pl.created_at, pl.updated_at ' ||
             'FROM product_lines pl ' ||
             'JOIN categories c ON pl.category_id = c.id WHERE 1=1';

    IF p_search_key IS NOT NULL AND p_search_key <> '' THEN
        query := query || ' AND (pl.title ILIKE ''%' || p_search_key || '%'' OR pl.description ILIKE ''%' || p_search_key || '%'')';
    END IF;

    IF p_category_name IS NOT NULL AND p_category_name <> '' THEN
        query := query || ' AND LOWER(c.name) = LOWER(''' || p_category_name || ''')';
    END IF;

    IF p_sort_by IS NOT NULL AND p_sort_order IS NOT NULL THEN
        query := query || ' ORDER BY ' || quote_ident(p_sort_by) || ' ' || p_sort_order;
    END IF;

    IF p_limit IS NOT NULL THEN
        query := query || ' LIMIT ' || p_limit;
    END IF;

    IF p_starting_after IS NOT NULL THEN
        query := query || ' OFFSET ' || p_starting_after;
    END IF;

    -- Log the query
    RAISE NOTICE 'Executing query: %', query;

    RETURN QUERY EXECUTE query;
END;
$BODY$;
