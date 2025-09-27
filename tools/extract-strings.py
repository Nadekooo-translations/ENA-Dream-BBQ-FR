import json
import os
import UnityPy
from UnityPy.helpers.TypeTreeGenerator import TypeTreeGenerator

folder = '/home/vincent/.steam/steam/steamapps/common/ENA Dream BBQ' 
yarnBundlePath = folder + '/ENA-4-DreamBBQ_Data/StreamingAssets/aa/StandaloneWindows64/yarndialogue_assets_all_7acf6b9d978e1fa4d72fa4cb2910adda.bundle'

if not os.path.isfile(yarnBundlePath):
    raise Exception('Yarn Bundle not found')

output = {}

yarn = UnityPy.load(yarnBundlePath)
yarnBundle = yarn.objects[2].read()
yarnLines = yarnBundle.m_Container[1][1].asset.read_typetree()['_stringTable']

output['yarn'] = {}

for i, key in enumerate(yarnLines['keys']):
    output['yarn'][key] = yarnLines['values'][i]


f = open('l10n/en.json', 'w')
f.write(json.dumps(output, indent=4))
f.close()

