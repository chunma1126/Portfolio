#pragma once
#include "Scene.h"
class DelayedCall;
class Player;
class Background;
class BossScene : public Scene
{
public:
	BossScene(wstring _sceneName);
	~BossScene();
	void Init() override;
	void Update() override;
public:
private:
	Player* m_player;
	Background* m_backGround;
};

