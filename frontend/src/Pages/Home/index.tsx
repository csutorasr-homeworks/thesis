import React, { useEffect, useState } from 'react';

export default function Home() {
  const [fleets, setFleets] = useState<{ id: string; name: string }[]>([]);
  useEffect(() => {
    (async () => {
      const response = await fetch('/api/fleets');
      setFleets(await response.json());
    })();
  }, []);
  return (
    <div>
      Home
      <ul>
        {fleets.map((x) => (
          <li key={x.id}>{x.name}</li>
        ))}
      </ul>
    </div>
  );
}
