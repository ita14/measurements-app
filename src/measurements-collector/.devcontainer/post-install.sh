#!/bin/bash

# Install generated api client
cd measurements-api-client && sudo python3 setup.py install ; cd -

# Install collector dependencies
python3 -m pip install -r requirements.txt

