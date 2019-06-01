from django.db import models
from django.db.models.signals import post_save
from django.contrib.auth.models import User
from django.dispatch import receiver

class Win(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE, related_name='win')
    wins = models.IntegerField(default=0)
    VRwins = models.IntegerField(default=0)
    CTRLwins = models.IntegerField(default=0)

@receiver(post_save, sender=User)
def create_user_profile(sender, instance, created, **kwargs):
    if created:
        Win.objects.create(user=instance)

@receiver(post_save, sender=User)
def save_user_profile(sender, instance, **kwargs):
    instance.win.save()
