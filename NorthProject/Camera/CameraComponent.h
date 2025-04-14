#pragma once
#include "Component.h"
class CameraComponent : public Component
{
public:
    CameraComponent();
    ~CameraComponent();

    void LateUpdate() override;
    void Render(HDC _hdc) override;

	void Shake(float _strength, float _duration);
    void ApplyShake(Vec2& _pos, float _dt);
private:
    float m_shakeStrength = 0.f;
    float m_shakeDuration = 0.f;
    bool  m_IsShake = false;
    float m_shakeTime = 0.f;
    std::random_device m_rd;
    std::mt19937 m_mt; 


};

