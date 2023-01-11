import React, { useMemo } from 'react';
import UPlotChart from './UPlotChart';
import { AlignedData, Options } from 'uplot';
import { useMeasure } from 'react-use';
import { Paper, Typography } from '@mui/material';
import { Sensor } from '../../generated/measurements-api-client';
import { useGetMeasurements } from '../../api/measurements.api';
import { Spinner } from '../../components';
import { useApplicationStore } from '../../stores/applicationStore';

const HEIGHT = 400;
const commonOpts = {
  cursor: {
    sync: {
      key: '1'
    }
  },
  scales: {
    x: {
      time: true
    }
  }
};

function getChart1Options(width: number, theme: string | undefined): Options {
  return {
    ...commonOpts,
    width: width,
    height: HEIGHT,
    series: [
      {
        value: '{YYYY}-{MM}-{DD} {HH}:{mm}:{ss}'
      },
      {
        label: 'Temperature',
        scale: 'Celsius',
        stroke: 'red',
        fill: 'rgba(255,0,0,0.1)'
      },
      {
        label: 'Humidity',
        scale: '%',
        stroke: 'blue',
        fill: 'rgba(255,0,0,0.1)'
      }
    ],
    axes: [
      {
        stroke: theme === 'dark' ? 'white' : 'black'
      },
      {
        scale: 'Celsius',
        side: 3,
        stroke: theme === 'dark' ? 'white' : 'black'
      },
      {
        scale: '%',
        side: 1,
        grid: { show: false },
        stroke: theme === 'dark' ? 'white' : 'black'
      }
    ]
  };
}

function getChart2Options(width: number, theme: string | undefined): Options {
  return {
    ...commonOpts,
    width: width,
    height: HEIGHT,
    series: [
      {
        value: '{YYYY}-{MM}-{DD} {HH}:{mm}:{ss}'
      },
      {
        label: 'Battery',
        scale: 'battery',
        stroke: 'green',
        fill: 'rgba(212,44,242,0.1)'
      },
      {
        label: 'Pressure',
        scale: 'pressure',
        stroke: 'yellow',
        fill: 'rgba(242,195,44,0.1)'
      }
    ],
    axes: [
      {
        stroke: theme === 'dark' ? 'white' : 'black'
      },
      {
        scale: 'battery',
        stroke: theme === 'dark' ? 'white' : 'black'
      },
      {
        scale: 'pressure',
        side: 1,
        grid: { show: false },
        stroke: theme === 'dark' ? 'white' : 'black'
      }
    ]
  };
}

export interface Props {
  sensor: Sensor;
  startTime: Date;
  endTime: Date;
}

function SensorGraph({ sensor, startTime, endTime }: Props) {
  const theme = useApplicationStore((state) => state.theme);
  const [ref, { width }] = useMeasure<HTMLDivElement>();
  const { isLoading, isError, data, error } = useGetMeasurements(sensor.id, startTime, endTime);

  const memoizedData = useMemo(() => {
    if (data === undefined || data.length === 0) {
      return {
        alignedData: [],
        summaryRow: `${sensor?.description} - No data`
      };
    }

    const alignedData: AlignedData[] = [
      [
        data?.map((x) => (x.time?.getTime() ?? 0) / 1000),
        data?.map((x) => x.temperature),
        data?.map((x) => x.humidity)
      ],
      [
        data?.map((x) => (x.time?.getTime() ?? 0) / 1000),
        data?.map((x) => x.battery),
        data?.map((x) => x.pressure)
      ]
    ];

    const latest = data[data.length - 1];
    // prettier-ignore
    const summaryRow = `${sensor?.description} ${latest?.temperature?.toFixed(2)} °C  - ${latest?.humidity?.toFixed(2)} %`;

    return { alignedData, summaryRow };
  }, [data, sensor]);

  if (isError) {
    return <div>error {error?.detail}</div>;
  }
  return (
    <Paper
      ref={ref}
      elevation={15}
      sx={{
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        margin: 2,
        padding: 2,
        minHeight: '500px'
      }}
    >
      {isLoading ? (
        <Spinner />
      ) : (
        <>
          <Typography variant="h5">{memoizedData.summaryRow}</Typography>
          <UPlotChart options={getChart1Options(width, theme)} data={memoizedData.alignedData[0]} />
          <UPlotChart options={getChart2Options(width, theme)} data={memoizedData.alignedData[1]} />
        </>
      )}
    </Paper>
  );
}

export default SensorGraph;
