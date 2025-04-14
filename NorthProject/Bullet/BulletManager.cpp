#include "pch.h"
#include "BulletManager.h"
#include "Object.h"
#include "Projectile.h"
#include "FollowProjectile.h"
#include "EnemyProjectile.h"
#include "TimeManager.h"
#include "EventManager.h"

void BulletManager::Init()
{
#pragma region directionAndSpeed
	m_speeds[0] = 111.00f;
	m_direction[0] = 90.00f;
	m_speeds[1] = 133.10f;
	m_direction[1] = 98.70f;
	m_speeds[2] = 152.04f;
	m_direction[2] = 107.37f;
	m_speeds[3] = 166.88f;
	m_direction[3] = 116.18f;
	m_speeds[4] = 176.00f;
	m_direction[4] = 125.28f;
	m_speeds[5] = 181.88f;
	m_direction[5] = 134.29f;
	m_speeds[6] = 181.50f;
	m_direction[6] = 143.31f;
	m_speeds[7] = 175.54f;
	m_direction[7] = 152.33f;
	m_speeds[8] = 165.63f;
	m_direction[8] = 161.38f;
	m_speeds[9] = 151.50f;
	m_direction[9] = 170.43f;
	m_speeds[10] = 136.35f;
	m_direction[10] = 180.18f;
	m_speeds[11] = 120.40f;
	m_direction[11] = 190.90f;
	m_speeds[12] = 106.45f;
	m_direction[12] = 203.68f;
	m_speeds[13] = 98.56f;
	m_direction[13] = 219.22f;
	m_speeds[14] = 99.10f;
	m_direction[14] = 235.97f;
	m_speeds[15] = 107.97f;
	m_direction[15] = 251.19f;
	m_speeds[16] = 124.58f;
	m_direction[16] = 262.83f;
	m_speeds[17] = 133.10f;
	m_direction[17] = 81.30f;
	m_speeds[18] = 152.04f;
	m_direction[18] = 72.63f;
	m_speeds[19] = 166.88f;
	m_direction[19] = 63.82f;
	m_speeds[20] = 176.00f;
	m_direction[20] = 54.72f;
	m_speeds[21] = 181.88f;
	m_direction[21] = 45.71f;
	m_speeds[22] = 181.50f;
	m_direction[22] = 36.69f;
	m_speeds[23] = 175.54f;
	m_direction[23] = 27.67f;
	m_speeds[24] = 165.63f;
	m_direction[24] = 18.62f;
	m_speeds[25] = 151.50f;
	m_direction[25] = 9.57f;
	m_speeds[26] = 136.35f;
	m_direction[26] = 359.82f;
	m_speeds[27] = 120.40f;
	m_direction[27] = 349.10f;
	m_speeds[28] = 106.45f;
	m_direction[28] = 336.32f;
	m_speeds[29] = 98.56f;
	m_direction[29] = 320.78f;
	m_speeds[30] = 99.10f;
	m_direction[30] = 304.03f;
	m_speeds[31] = 107.97f;
	m_direction[31] = 288.81f;
	m_speeds[32] = 124.58f;
	m_direction[32] = 277.17f;
	m_speeds[33] = 147.85f;
	m_direction[33] = 270.05f;
#pragma endregion
}

void BulletManager::Update()
{
	if (m_isSpinShot)
	{
		if (m_spinEndTimer >= m_spinEndTime)
		{
			ResetSpinShot();
		}
		else
		{
			ApplySpinShot();
		}
	}

	if (m_isRoseShot)
	{
		if (m_roseShotEnd)
		{
			ResetRoseShot();

			for (auto item : m_roseVec)
			{
				RoseSpinShotComplete(item);
			}

			m_roseVec.clear();
		}
		else
		{
			ApplyRoseSpinShot();
		}
	}


}

void BulletManager::ResetSpinShot()
{
	m_spinEndTimer = 0;
	m_spinShotTimer = 0;
	m_isSpinShot = false;
}

void BulletManager::ResetRoseShot()
{
	m_isRoseShot = false;
	m_roseShotEnd = false;
	m_roseEndTimer = 0;
	m_roseShotTimer = 0;
}

void BulletManager::RoseSpinShotComplete(Projectile* item)
{
	item->SetSpeed(m_roseShotBulletSpeed);
	Vec2 currentProjectile = item->GetPos();

	float centerX = SCREEN_WIDTH / 2;
	float centerY = SCREEN_HEIGHT / 2;

	float relativeX = currentProjectile.x - centerX;
	float relativeY = currentProjectile.y - centerY;

	relativeX = relativeX == 0 ? -1 : relativeX;
	relativeY = relativeY == 0 ? -1 : relativeY;

	Vec2 direction = { relativeX, relativeY };
	direction.Normalize();
	item->SetDir(direction);
}

void BulletManager::BasicShot(Vec2 _pos, Scene* _scene, float _bulletSpeed, Vec2 _dir)
{
	Projectile* p = new EnemyProjectile;

	p->SetPos(_pos);
	p->SetDir(_dir);
	p->SetSpeed(_bulletSpeed);
	p->SetName(L"Projectile");
	p->SetTag(TagEnum::EnemyProjectile);

	_scene->AddObject(p, LAYER::PROJECTILE);
}

void BulletManager::CircleShot(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed)
{
	for (int i = 0; i < 360; i += _interval)
	{
		Projectile* pObj = new EnemyProjectile;
		pObj->SetPos(_pos);
		pObj->SetSize({ 400.f,400.f });
		pObj->SetSpeed(_bulletSpeed);

		pObj->SetName(L"Projectile");
		pObj->SetTag(TagEnum::EnemyProjectile);


		_scene->AddObject(pObj, LAYER::PROJECTILE);

		float angle = i * (PI / 180.0f);
		pObj->SetDir({ cosf(angle), sinf(angle) });
	}
}

void BulletManager::CircleShotGoToTarget(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed, Object* _target, float _changeTime)
{
	for (int i = 0; i < 360; i += _interval)
	{
		FollowProjectile* pObj = new FollowProjectile;

		pObj->SetPos(_pos);
		pObj->SetSize({ 400.f,400.f });
		pObj->SetSpeed(_bulletSpeed);
		pObj->SetChangeTime(_changeTime);
		pObj->SetName(L"Projectile");
		pObj->SetTarget(_target);
		pObj->SetTag(TagEnum::EnemyProjectile);

		_scene->AddObject(pObj, LAYER::PROJECTILE);

		float angle = i * (PI / 180.0f);
		pObj->SetDir({ cosf(angle), sinf(angle) });
	}
}

void BulletManager::SpinShot(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed, float& _spinAngle, float _turnSpeed, float _endTime)
{
	m_spinShotPos = _pos;
	m_spinShotScene = _scene;
	m_spinShotInterval = _interval;
	m_spinShotBulletSpeed = _bulletSpeed;

	m_isSpinShot = true;

	m_spinAngle = _spinAngle;
	m_spinTurnSpeed = _turnSpeed;
	m_spinEndTime = _endTime;

}

void BulletManager::ApplySpinShot()
{
	m_spinShotTimer += fDT;
	if (m_spinShotTimer < m_spinShotInterval) return;

	m_spinEndTimer += m_spinShotTimer;
	m_spinShotTimer = 0.0f;

	m_spinAngle += m_spinTurnSpeed * fDT;
	if (m_spinAngle >= 360.0f) m_spinAngle -= 360.0f;

	float angleInRadians = m_spinAngle * (PI / 180.0f);

	Projectile* pObj = new EnemyProjectile;

	pObj->SetPos(m_spinShotPos);
	pObj->SetSize({ 400.f, 400.f });
	pObj->SetSpeed(m_spinShotBulletSpeed);
	pObj->SetTag(TagEnum::EnemyProjectile);

	Vec2 dir = { cosf(angleInRadians), sinf(angleInRadians) };
	pObj->SetDir(dir);

	m_spinShotScene->AddObject(pObj, LAYER::PROJECTILE);
}

void BulletManager::ShapeShot(Vec2 _pos, Scene* _scene, float _bulletSpeed, Vec2 _dir, int _vertex, float _size, float _rotationSpeed)
{
	float angleIncrement = 360.0f / _vertex;
	float radius = _size;

	for (int i = 0; i < _vertex; ++i)
	{
		float angleDegrees = i * angleIncrement;
		float angleRadians = angleDegrees * (M_PI / 180.0f);

		Projectile* pObj = new EnemyProjectile;

		float x = _pos.x + radius * cosf(angleRadians);
		float y = _pos.y + radius * sinf(angleRadians);

		pObj->SetPos({ x, y });
		pObj->SetSize({ 10,10 });
		pObj->SetName(L"Projectile");
		pObj->SetDir(_dir);
		pObj->SetSpeed(_bulletSpeed);
		pObj->SetTag(TagEnum::EnemyProjectile);
		_scene->AddObject(pObj, LAYER::PROJECTILE);
	}
}

void BulletManager::HeartShot(Vec2 _pos, Scene* _scene, float _bulletSpeed)
{
	for (int i = 0; i < 34; ++i)
	{
		Projectile* p = new EnemyProjectile;

		p->SetPos(_pos);
		p->SetSpeed(m_speeds[i] + _bulletSpeed);
		p->SetSize({ 200,200 });
		p->SetTag(TagEnum::EnemyProjectile);
		float angleDegrees = m_direction[i] + m_heartRotation;
		float angleRadians = angleDegrees * (M_PI / 180.0f);

		float speed = m_speeds[i] / 50.0f;

		float vx = cos(angleRadians) * speed;
		float vy = sin(angleRadians) * speed;

		p->SetDir({ vx, vy });

		_scene->AddObject(p, LAYER::PROJECTILE);
	}
}

void BulletManager::HeartShotGoToTarget(Vec2 _pos, Scene* _scene, float _bulletSpeed, Object* _target, float _changeTime)
{
	for (int i = 0; i < 34; ++i)
	{
		FollowProjectile* p = new FollowProjectile;

		p->SetTarget(_target);
		p->SetChangeTime(_changeTime);
		p->SetPos(_pos);
		p->SetSpeed(m_speeds[i] + _bulletSpeed);
		p->SetSize({ 200,200 });
		p->SetTag(TagEnum::EnemyProjectile);

		float angleDegrees = m_direction[i] + m_heartRotation;
		float angleRadians = angleDegrees * (M_PI / 180.0f);

		float speed = m_speeds[i] / 50.0f;
		float vx = cos(angleRadians) * speed;
		float vy = sin(angleRadians) * speed;

		p->SetDir({ vx, vy });

		_scene->AddObject(p, LAYER::PROJECTILE);
	}
}

void BulletManager::HeartDataInit(float _rotation)
{
	m_heartRotation = _rotation;
}

void BulletManager::RoseShot(Scene* _scene, float _interval, float _bulletSpeed, Vec2 _dir, int _petals, float _size)
{
	int vertex = 360;
	float k = static_cast<float>(_petals);

	for (int i = 0; i < vertex; ++i)
	{
		float angleDegrees = i;
		float angleRadians = angleDegrees * (M_PI / 180.0f);

		float radius = _size * sinf(k * angleRadians);

		Projectile* pObj = new EnemyProjectile;

		float x = SCREEN_WIDTH * 0.5f + radius * cosf(angleRadians);
		float y = SCREEN_HEIGHT * 0.2f + radius * sinf(angleRadians);

		pObj->SetPos({ x, y });
		pObj->SetName(L"Projectile");
		pObj->SetDir(_dir);
		pObj->SetSpeed(_bulletSpeed);
		pObj->SetTag(TagEnum::EnemyProjectile);

		_scene->AddObject(pObj, LAYER::PROJECTILE);
	}
}

void BulletManager::RoseSpinShot(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed, Object* _target, int _petals, float _size, float _endTime, float _rotationSpeed)
{
	m_isRoseShot = true;
	m_roseShotEnd = false;

	m_roseShotPos = _pos;
	m_roseShotScene = _scene;
	m_roseShotInterval = _interval;
	m_roseShotBulletSpeed = _bulletSpeed;

	m_target = _target;
	m_roseSize = _size;
	m_rosePetals = _petals;
	m_roseEndTime = _endTime;
	m_roseRotationSpeed = _rotationSpeed;
}

void BulletManager::ApplyRoseSpinShot()
{
	m_roseShotTimer += fDT;

	if (m_roseShotTimer < m_roseShotInterval) return;

	m_roseEndTimer += m_roseShotTimer;
	m_roseShotTimer = 0;

	if (m_roseEndTimer >= m_roseEndTime)
		m_roseShotEnd = true;

	int vertex = 360;
	static int currentIndex = 0;

	float angleDegrees = currentIndex;
	float angleRadians = angleDegrees * (M_PI / 180.0f);

	float radius = m_roseSize * sinf(m_rosePetals * angleRadians);

	Projectile* pObj = new EnemyProjectile;

	float x = SCREEN_WIDTH / 2 + radius * cosf(angleRadians);
	float y = SCREEN_HEIGHT / 2 + radius * sinf(angleRadians);

	pObj->SetPos({ x, y });
	pObj->SetSpeed(0);
	pObj->SetName(L"Projectile");
	pObj->SetTag(TagEnum::EnemyProjectile);
	m_roseVec.push_back(pObj);

	m_roseShotScene->AddObject(pObj, LAYER::PROJECTILE);

	currentIndex = (++currentIndex) % vertex;


	for (int i = 0; i < m_roseVec.size(); i++)
	{
		Vec2 currentProjectile = m_roseVec[i]->GetPos();

		float centerX = SCREEN_WIDTH / 2;
		float centerY = SCREEN_HEIGHT / 2;

		float relativeX = currentProjectile.x - centerX;
		float relativeY = currentProjectile.y - centerY;

		float rotationAngle = m_roseRotationSpeed * fDT;

		float cosTheta = cosf(rotationAngle);
		float sinTheta = sinf(rotationAngle);

		float newX = centerX + (relativeX * cosTheta - relativeY * sinTheta);
		float newY = centerY + (relativeX * sinTheta + relativeY * cosTheta);

		m_roseVec[i]->SetPos({ newX, newY });
	}
}


