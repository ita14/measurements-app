import logging
import time
import argparse
import json
#from ruuvitag_sensor.ruuvi import RuuviTagSensor
from datetime import datetime, timedelta


parser = argparse.ArgumentParser()

# Configure logging
logger = logging.getLogger("ruuvi_data_logger")
logger.setLevel(logging.DEBUG)
streamHandler = logging.StreamHandler()
formatter = logging.Formatter(
    '%(asctime)s - %(name)s - %(levelname)s - %(message)s')
streamHandler.setFormatter(formatter)
logger.addHandler(streamHandler)


macs = [
    'F3:D9:E8:28:41:5B',
    'C6:4C:96:B3:20:7E',
    'D8:4E:92:B8:86:22',
    'EA:04:8D:66:BE:87'
]


# Compose and publish messages
while (1):
    logger.info('Hello')
    print('hello print')
    time.sleep(2)
