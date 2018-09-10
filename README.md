
[![Docker Automated build](https://img.shields.io/docker/automated/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![Docker Build Status](https://img.shields.io/docker/build/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![GitHub issues](https://img.shields.io/github/issues/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/issues)
[![GitHub forks](https://img.shields.io/github/forks/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/network)
[![GitHub stars](https://img.shields.io/github/stars/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/stargazers)
[![GitHub license](https://img.shields.io/github/license/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/blob/master/LICENSE)

# 支持的云存储
[阿里云OSS](https://www.aliyun.com/product/oss)  
[腾讯云COS](https://cloud.tencent.com/product/cos)  

# 使用方法(Docker)
## 运行
```
docker run --rm -v /xxx/yyy:/data -v /etc/backup2cloud:/conf sanjusss/backup2cloud run -c /conf/backup2cloud.json
```
运行后程序将加载配置文件backup2cloud.json。  
注意文件夹的挂载。

## 查看示例配置文件
```
docker run --rm -v /D/share:/conf sanjusss/backup2cloud eg -s /conf/eg.json
```
运行后将在D:\share\eg.json生成示例配置文件。  
如果省略`-s /conf/eg.json`，将不保存文件，仅在命令行下输出。
示例文件如下：
```
[
  {
    "name": "上传到 aliyun",
    "provider": "aliyun",
    "path": "/data",
    "crontab": [
      "0,30 * * * * ?"
    ],
    "uploader": {
      "endpoint": "oss-cn-hangzhou.aliyuncs.com",
      "id": "阿里云AccessKeyId",
      "secret": "阿里云AccessKeySecret",
      "bucket": "backup",
      "path": "data/some",
      "timeout": 200000,
      "Tips": "endpoint：地域节点（可以在控制台查看）；id：阿里云AccessKeySecret；secret：阿里云AccessKeySecret；bucket：存储空间名path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件；timeout：上传超时时间，单位毫秒"
    },
    "Tips": "name：任务名称；provider：上传服务提供商；path：需要备份的文件夹或文件在本地的路径；crontab：启动备份的时间集合，可以参考http://cron.qqe2.com/，使用时需要注意时区；uploader：上传设置"
  },
  {
    "name": "上传到 qcloud",
    "provider": "qcloud",
    "path": "/data",
    "crontab": [
      "0,30 * * * * ?"
    ],
    "uploader": {
      "appId": "123456",
      "secretId": "xxxx",
      "secretKey": "yyyy",
      "url": "https://backup-123456.cos.ap-chengdu.myqcloud.com/test",
      "Tips": "appId：腾讯云 APPID（可以在控制台查看）；secretId：腾讯云 SecretId（可以在控制台查看）；secretKey：腾讯云 SecretKey（可以在控制台查看）；url：绝对路径，例如\"https://backup-123456.cos.ap-chengdu.myqcloud.com/test\"，最终会生成类似\"https://backup-123456.cos.ap-chengdu.myqcloud.com/test201809092054.zip\"之类的文件，可以在 控制台-对象存储-存储桶列表-点击实际的桶名称-基础设置-访问域名 中找到url的前部分，后部分是文件相对于桶的路径"
    },
    "Tips": "name：任务名称；provider：上传服务提供商；path：需要备份的文件夹或文件在本地的路径；crontab：启动备份的时间集合，可以参考http://cron.qqe2.com/，使用时需要注意时区；uploader：上传设置"
  }
]
```
实际的配置文件中没有Tips一项。  

## 查看命令帮助
```
docker run --rm sanjusss/backup2cloud --help
```

## 查看软件版本
```
docker run --rm sanjusss/backup2cloud --version
```

# 引用项目
https://www.newtonsoft.com/json  
https://github.com/vla/aliyun-oss-csharp-sdk  
https://github.com/commandlineparser/commandline  
https://github.com/zhengchun/qcloud-sdk-net  
https://github.com/adamhathcock/sharpcompress  
https://www.quartz-scheduler.net  
