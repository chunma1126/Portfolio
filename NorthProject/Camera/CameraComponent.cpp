#include "pch.h"
#include "CameraComponent.h"
#include "TimeManager.h"
#include "ResourceManager.h"
#include "SceneManager.h"
#include "Scene.h"
#include "Object.h"
#include "Texture.h"
#include "Camera.h"
CameraComponent::CameraComponent()
{
	m_mt.seed(m_rd());
}

CameraComponent::~CameraComponent()
{
}

void CameraComponent::Shake(float _strength, float _duration)
{
	m_mt.seed(m_rd());

	m_shakeStrength = _strength;
	m_shakeDuration = _duration;
	m_IsShake = true;
}

void CameraComponent::ApplyShake(Vec2& _pos, float _dt)
{
	std::uniform_int_distribution<int> shakex(-m_shakeStrength, m_shakeStrength);
	std::uniform_int_distribution<int> shakey(-m_shakeStrength, m_shakeStrength);
	_pos.x += shakex(m_mt);
	_pos.y += shakex(m_mt);

	m_shakeTime += _dt;
	if (m_shakeTime >= m_shakeDuration)
	{
		m_IsShake = false;
		m_shakeTime = 0.f;
	}
}

void CameraComponent::LateUpdate()
{
	Vec2 pos = GetOwner()->GetPos();
	
	if (m_IsShake) 
	{
		ApplyShake(pos, fDT);
	}

	GET_SINGLE(Camera)->SetCameraPos(pos);
}

void CameraComponent::Render(HDC _hdc)
{

}
