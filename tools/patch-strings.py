import json
import os
import UnityPy
from UnityPy.helpers.TypeTreeGenerator import TypeTreeGenerator

folder = '/home/vincent/.steam/steam/steamapps/common/ENA Dream BBQ' 
yarnBundlePath = folder + '/ENA-4-DreamBBQ_Data/StreamingAssets/aa/StandaloneWindows64/yarndialogue_assets_all_6cde9116b898ddabb0291f5836395399.bundle'

if not os.path.isfile(yarnBundlePath):
    raise Exception('Yarn Bundle not found')

yarn = UnityPy.load(yarnBundlePath)
yarnBundle = yarn.objects[2].read()
yarnAsset = yarnBundle.m_Container[1][1].asset
yarnLines = yarnAsset.read_typetree()
open('/tmp/source.json', 'w').write(json.dumps(yarnLines))

f = open('l10n/fr.json', 'r')
strings = json.load(f)

for key, target in strings['yarn'].items():
    idx = yarnLines['_stringTable']['keys'].index(key)
    yarnLines['_stringTable']['values'][idx] = target

open('/tmp/target.json', 'w').write(json.dumps(yarnLines))

yarnAsset.read().object_reader.save_typetree(yarnLines)
yarn.save()

