const KEY_PREFIX = "veh_filters";

export function loadFilters<T>(key: string, fallback: T): T {
  try {
    const raw = localStorage.getItem(`${KEY_PREFIX}:${key}`);
    return raw ? { ...fallback, ...JSON.parse(raw) } : fallback;
  } catch {
    return fallback;
  }
}

export function saveFilters<T>(key: string, value: T) {
  try {
    localStorage.setItem(`${KEY_PREFIX}:${key}`, JSON.stringify(value));
  } catch {}
}