from dataclasses import dataclass
import json
from base64 import b64decode
from struct import unpack

@dataclass
class ObjectType:
	m_AssemblyName: str
	m_ClassName: str

@dataclass
class ProviderData:
	m_Id: str
	m_ObjectType: ObjectType
	m_Data: str

@dataclass
class Catalog:
	m_LocatorId: str
	m_InstanceProviderData: ProviderData
	m_SceneProviderData: ProviderData
	m_ResourceProviderData: list[ProviderData]
	m_ProviderIds: list[str]
	m_InternalIds: list[str]
	m_KeyDataString: str
	m_BucketDataString: str
	m_EntryDataString: str
	m_ExtraDataString: str
	m_resourceTypes: list[ObjectType]
	m_InternalIdPrefixes: list[str]

with open('/home/vincent/.steam/steam/steamapps/common/ENA Dream BBQ/ENA-4-DreamBBQ_Data/StreamingAssets/aa/catalog.json', 'r') as f:
	cat: Catalog = Catalog(**json.loads(f.read()))
	keyData = b64decode(cat.m_KeyDataString)
	print(keyData)
	count = unpack('<L', keyData)
	print(count)

