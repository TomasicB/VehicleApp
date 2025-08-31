import type { ChangeEvent } from "react";

interface Props {
  q: string;
  onQChange: (v: string) => void;
  page: number;
  pageSize: number;
  total: number;
  onPageChange: (p: number) => void;
}

export default function ListBar({ q, onQChange, page, pageSize, total, onPageChange }: Props) {
  const pages = Math.max(1, Math.ceil(total / pageSize));
  const prev = () => onPageChange(Math.max(1, page - 1));
  const next = () => onPageChange(Math.min(pages, page + 1));
  const handleQ = (e: ChangeEvent<HTMLInputElement>) => onQChange(e.target.value);

  return (
    <div style={{ display: "flex", gap: 12, alignItems: "center", marginBottom: 12 }}>
      <input value={q} onChange={handleQ} placeholder="Search…" />
      <div style={{ marginLeft: "auto", display: "flex", gap: 8, alignItems: "center" }}>
        <button onClick={prev} disabled={page <= 1}>Prev</button>
        <span>{page} / {pages}</span>
        <button onClick={next} disabled={page >= pages}>Next</button>
      </div>
    </div>
  );
}