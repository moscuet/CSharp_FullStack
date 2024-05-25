CREATE OR REPLACE FUNCTION get_product_lines(
    p_limit INT, 
    p_starting_after INT, 
    p_sort_by TEXT, 
    p_sort_order TEXT, 
    p_search_key TEXT, 
    p_category_name TEXT
)
RETURNS SETOF product_lines AS $$
DECLARE
    query TEXT;
BEGIN
    query := 'SELECT pl.*, c.name as category_name FROM product_lines pl ' ||
             'JOIN categories c ON pl.category_id = c.id WHERE 1=1';

    IF p_search_key IS NOT NULL THEN
        query := query || ' AND (pl.title LIKE ''%' || p_search_key || '%'' OR pl.description LIKE ''%' || p_search_key || '%'')';
    END IF;

    IF p_category_name IS NOT NULL THEN
        query := query || ' AND c.name = ''' || p_category_name || '''';
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

    RETURN QUERY EXECUTE query;
END;
$$ LANGUAGE plpgsql;
