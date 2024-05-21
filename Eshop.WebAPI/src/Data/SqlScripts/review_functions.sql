
CREATE OR REPLACE FUNCTION get_reviews(p_limit INT, p_starting_after INT, p_sort_by TEXT, p_sort_order TEXT, p_search_key TEXT)
RETURNS SETOF reviews AS $$
DECLARE
    query TEXT;
BEGIN
    query := 'SELECT * FROM reviews WHERE 1=1';

    IF p_search_key IS NOT NULL THEN
        query := query || ' AND comment LIKE ''%' || p_search_key || '%''';
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
