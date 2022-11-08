FROM python:3.9

COPY . .
RUN pip install -r requirements.txt
RUN cd measurements-api-client && python3 setup.py install ; cd -

CMD ["python", "src/collect_ruuvi_data.py"]
