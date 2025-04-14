#include "pch.h"
#include "BossScene.h"
#include "ResourceManager.h"
#include "CollisionManager.h";
#include "BulletManager.h"
#include "InputManager.h"
#include "TimeManager.h"
#include "UIManager.h"
#include "EventManager.h"
#include "Background.h"
#include "AllEnemies.h"
#include "Player.h"
#include "EnemyProjectile.h"
#include "DelayedCall.h"
#include "EnemySpawnEventManager.h"
void BossScene::Init()
{
	GET_SINGLE(EnemySpawnEventManager)->Clear();
	//m_finalBossAllowed = false;
	Background* m_backGround = new Background;
	AddObject(m_backGround, LAYER::BACKGROUND);


	m_player = new Player;
	AddObject(m_player, LAYER::PLAYER);

	{
		GET_SINGLE(CollisionManager)->CheckLayer(LAYER::PROJECTILE, LAYER::ENEMY);
		GET_SINGLE(CollisionManager)->CheckLayer(LAYER::PROJECTILE, LAYER::PLAYER);
		GET_SINGLE(CollisionManager)->CheckLayer(LAYER::PLAYER, LAYER::ENEMY);
		GET_SINGLE(CollisionManager)->CheckLayer(LAYER::PLAYER, LAYER::ITEM);
	}
	{
		GET_SINGLE(UIManager)->SetActiveChild(L"FirstScore", true);
		GET_SINGLE(UIManager)->SetActiveChild(L"SecondeScore", true);
		GET_SINGLE(UIManager)->SetActiveChild(L"ThirdScore", true);
	}
	{
		GET_SINGLE(UIManager)->SetActiveChild(L"PlayerHeart1", true);
		GET_SINGLE(UIManager)->SetActiveChild(L"PlayerHeart2", true);
		GET_SINGLE(UIManager)->SetActiveChild(L"PlayerHeart3", true);

	}
	GET_SINGLE(ResourceManager)->Stop(SOUND_CHANNEL::BGM);
	GET_SINGLE(ResourceManager)->LoadSound(L"InGameBGM", L"Sound\\InGameBGM.mp3", true);
	GET_SINGLE(ResourceManager)->PlayAudio(L"InGameBGM");

	new DelayedCall(1.f, { {SCREEN_WIDTH * 0.2f , 5.f }, EnemyType::TrashMob2, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 7 });
	new DelayedCall(1.f, { {SCREEN_WIDTH * 0.5f , 5.f }, EnemyType::TrashMob2, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 7 });
	new DelayedCall(1.f, { {SCREEN_WIDTH * 0.8f , 5.f }, EnemyType::TrashMob2, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 7 });
	
	new DelayedCall(7.2f, { {SCREEN_WIDTH * 0.2f , 5.f }, EnemyType::TrashMob1, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 10 });
	new DelayedCall(7.2f, { {SCREEN_WIDTH * 0.8f , 5.f }, EnemyType::TrashMob1, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 10 });
	
	new DelayedCall(12.6f, { {SCREEN_WIDTH * 0.2f , -100.f }, EnemyType::TrashMob1, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 10 });
	new DelayedCall(12.6f, { {SCREEN_WIDTH * 0.2f , SCREEN_HEIGHT * 0.2f }, EnemyType::TrashMob1, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 9 });
	new DelayedCall(12.6f, { {SCREEN_WIDTH * 0.8f , -100.f }, EnemyType::TrashMob1, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 10 });
	new DelayedCall(12.6f, { {SCREEN_WIDTH * 0.8f , SCREEN_HEIGHT * 0.2f }, EnemyType::TrashMob1, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 9 });
	
	new DelayedCall(18.7f, { {SCREEN_WIDTH * 0.2f , 5.f }, EnemyType::TrashMob3, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 150 });
	//new DelayedCall(0.f, { {SCREEN_WIDTH * 0.2f , 5.f }, EnemyType::TrashMob3, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 1 });
}

void BossScene::Update()
{
	Scene::Update();
	if (GET_SINGLE(EventManager)->GetBossEnter())
	{
		GET_SINGLE(EventManager)->SetBossEnter(false);
		new DelayedCall(2.5f, { {SCREEN_WIDTH * 0.5f, -210.f}, EnemyType::MidBoss, L"EnemySheetBlue", L"Texture\\EnemySheet_Blue.bmp", 980 });
	}
}


BossScene::BossScene(const wstring _sceneName)
	: Scene(_sceneName)
{

}


BossScene::~BossScene()
{
	//delete m_backGround;
}
