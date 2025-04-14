#pragma once
#include "Enemy.h"
class TrashMob2 : public Enemy
{
public:
	TrashMob2(const wstring& _key, const wstring& _path);
	void Update() override;

	
};

