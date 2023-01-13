import React from 'react';
import SensorGraph from './SensorGraph';
import { Backdrop, CircularProgress, Container, Box } from '@mui/material';
import { useGetSensors } from '../../api/sensors.api';
import DateRangeSelect from './DateRangeSelect';
import { useMeasurementsStore } from '../../stores/measurementsStore';

function Sensors() {
  const { isLoading, isError, data, error } = useGetSensors();
  const { dateRange } = useMeasurementsStore();

  return (
    <Container>
      <Backdrop open={isLoading}>
        <CircularProgress color="inherit" />
      </Backdrop>
      {isError && <div>{error?.detail}</div>}

      {!isLoading && (
        <>
          <DateRangeSelect />
          <Box sx={{ padding: 2 }}>
            {data?.map((s) => (
              <SensorGraph
                key={s.id}
                sensor={s}
                startTime={dateRange.start}
                endTime={dateRange.end}
              />
            ))}
          </Box>
        </>
      )}
    </Container>
  );
}

export default Sensors;
