import React, { useCallback } from 'react';
import { Button } from 'react-bootstrap';

const supported = 'geolocation' in navigator;

export default function LocationProvider({
  setLocationData,
}: {
  setLocationData: (value: { longitude: number; latitude: number }) => void;
}): JSX.Element {
  const getLocation = useCallback(() => {
    navigator.geolocation.getCurrentPosition((position) => {
      setLocationData({
        latitude: position.coords.latitude,
        longitude: position.coords.longitude,
      });
    });
  }, [setLocationData]);
  if (supported) {
    return (
      <Button style={{ width: '100%' }} onClick={getLocation}>
        Get from GPS
      </Button>
    );
  }
  return <div>Setting location from GPS is not supported.</div>;
}
