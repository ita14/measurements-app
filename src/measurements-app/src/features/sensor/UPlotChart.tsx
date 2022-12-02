import React, { useEffect, useRef } from 'react';
import uPlot, { AlignedData, Options } from 'uplot';
import 'uplot/dist/uPlot.min.css';

export interface Props {
  options: Options;
  data: AlignedData;
}

function UPlotChart({ options, data }: Props) {
  const chartDivEl = useRef(null);
  useEffect(() => {
    const chart = new uPlot(options, data, chartDivEl.current ?? undefined);

    return () => {
      chart.destroy();
    };
  }, [options, data]);

  return <div ref={chartDivEl}></div>;
}

export default UPlotChart;
