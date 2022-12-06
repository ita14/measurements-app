import React, { useState } from 'react';
import SensorGraph from './SensorGraph';
import { DateRange } from 'materialui-daterange-picker';
import DateRangeSelect from './DateRangeSelect';
import { subDays, subMonths, subWeeks } from 'date-fns';
import {
  Backdrop,
  CircularProgress,
  Container,
  ToggleButton,
  ToggleButtonGroup,
  Box
} from '@mui/material';
import { useGetSensors } from '../../api/sensors.api';

const enum Ranges {
  Day,
  Week,
  Month,
  Custom
}

function Sensors() {
  const [rangeSelect, setRangeSelect] = useState(Ranges.Day);
  const [dateRange, setDateRange] = useState<DateRange>({
    startDate: subDays(new Date(), 1),
    endDate: new Date()
  });
  const [isOpen, setIsOpen] = useState(false);
  const { isLoading, isError, data, error } = useGetSensors();

  const handleRangeSelect = (
    event: React.MouseEvent<HTMLElement>,
    newValue: Ranges
  ) => {
    if (newValue !== null) {
      setRangeSelect(newValue);
    } else {
      // Update time range i.e. refresh
      newValue = rangeSelect;
    }

    if (newValue === Ranges.Custom) {
      return;
    }

    const endDate = new Date();
    let startDate;

    switch (newValue) {
      case Ranges.Day:
        startDate = subDays(endDate, 1);
        break;
      case Ranges.Week:
        startDate = subWeeks(endDate, 1);
        break;
      case Ranges.Month:
        startDate = subMonths(endDate, 1);
        break;
    }

    setDateRange({ startDate, endDate });
  };

  return (
    <Container>
      <Backdrop open={isLoading}>
        <CircularProgress color="inherit" />
      </Backdrop>
      {isError && <div>{error?.detail}</div>}

      {!isLoading && (
        <Box sx={{ padding: 2 }}>
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'flex-start',
              padding: 'theme.spacing(2)'
            }}
          >
            <ToggleButtonGroup
              color="primary"
              value={rangeSelect}
              exclusive
              size="small"
              onChange={handleRangeSelect}
            >
              <ToggleButton value={Ranges.Day}>Past day</ToggleButton>
              <ToggleButton value={Ranges.Week}>Week</ToggleButton>
              <ToggleButton value={Ranges.Month}>Month</ToggleButton>
              <ToggleButton value={Ranges.Custom}>Custom</ToggleButton>
            </ToggleButtonGroup>
            <Box sx={{ display: 'float' }}>
              <DateRangeSelect
                isOpen={isOpen}
                dateRange={dateRange}
                onChange={setDateRange}
                onToggleOpen={() => setIsOpen(!isOpen)}
              />
            </Box>
          </Box>

          {data?.map((s) => (
            <SensorGraph
              key={s.id}
              sensor={s}
              startTime={dateRange.startDate ?? new Date()}
              endTime={dateRange.endDate ?? new Date()}
            />
          ))}
        </Box>
      )}
    </Container>
  );
}

export default Sensors;
