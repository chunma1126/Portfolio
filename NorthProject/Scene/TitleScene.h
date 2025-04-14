#pragma once
#include "Scene.h"
class TitleScene : public Scene
{
public:
	virtual void Init() override;
	virtual void Update() override;

private :
	int dir = -1;
};

