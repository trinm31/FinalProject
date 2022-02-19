import React, { useState, useRef, useCallback } from "react";
import useSearchBook from "../Hooks/useSearchBook";

export default function Testinfinityscroll() {
    const [pageNum, setPageNum] = useState(1);
    const { isLoading, error, courses, hasMore } = useSearchBook(pageNum);

    const observer = useRef();
    const lastBookElementRef = useCallback(
        (node) => {
            if (isLoading) return;
            if (observer.current) observer.current.disconnect();
            observer.current = new IntersectionObserver((entries) => {
                if (entries[0].isIntersecting && hasMore) {
                    setPageNum((prev) => prev + 1);
                }
            });
            if (node) observer.current.observe(node);
        },
        [isLoading, hasMore]
    );
    

    return (
        <div className="App">
            {courses.map((book, i) => {
                if (courses.length === i + 1) {
                    return (
                        <div key={i} ref={lastBookElementRef}>
                            {book.name}
                        </div>
                    );
                } else {
                    return <div key={i}>{book.name}</div>;
                }
            })}
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </div>
    );
}
