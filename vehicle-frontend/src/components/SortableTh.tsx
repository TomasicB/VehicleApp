import React from "react";

export type SortDirection = "asc" | "desc" | null;

interface SortableThProps {
  field: string;
  label: string;
  current: string | null;
  direction: SortDirection;
  onSort: (field: string) => void;
}

const SortableTh: React.FC<SortableThProps> = ({
  field,
  label,
  current,
  direction,
  onSort,
}) => {
  const isActive = current === field;

  const renderArrow = () => {
    if (!isActive) return "?";
    return direction === "asc" ? "↑" : "↓";
  };

  const handleClick = () => {
    onSort(field);
  };

  return (
    <th onClick={handleClick} style={{ cursor: "pointer", userSelect: "none" }}>
      {label} {renderArrow()}
    </th>
  );
};

export default SortableTh;