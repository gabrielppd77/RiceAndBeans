import { useVersion } from "../@hooks/useVersion";

export function Version() {
  const { data } = useVersion();

  return <>{JSON.stringify(data || {})}</>;
}
