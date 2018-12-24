
[![Docker Build Status](https://img.shields.io/docker/build/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![Build status](https://ci.appveyor.com/api/projects/status/9sa1mtm53jxket5t?svg=true)](https://ci.appveyor.com/project/sanjusss/backup2cloud)
[![GitHub release](https://img.shields.io/github/release/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/releases/latest)
[![GitHub license](https://img.shields.io/github/license/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/blob/master/LICENSE)  
[![GitHub tag](https://img.shields.io/github/tag/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/tags)
[![GitHub issues](https://img.shields.io/github/issues/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/issues)
[![GitHub forks](https://img.shields.io/github/forks/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/network)
[![GitHub stars](https://img.shields.io/github/stars/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/stargazers)
[![GitHub repo size in bytes](https://img.shields.io/github/repo-size/sanjusss/backup2cloud.svg)](#)

# 用途
定时将服务器上的文件或文件夹备份到网络云存储（OSS或S3）。  
可以设置在多个时间点，备份多个文件或文件夹到多个网络云存储。  

# 支持的云存储

| 服务提供商 | 产品名称 | 是否支持 | 备注 |
|---|---|---|----|
|[阿里云](https://www.aliyun.com)|[OSS](https://www.aliyun.com/product/oss)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[腾讯云](https://cloud.tencent.com)|[COS](https://cloud.tencent.com/product/cos)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[百度云](https://cloud.baidu.com)|[BOS](https://cloud.baidu.com/product/bos.html)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[七牛云](https://www.qiniu.com)|[对象存储](https://www.qiniu.com/products/kodo)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[AWS](https://amazonaws-china.com/cn/)|[S3](https://amazonaws-china.com/cn/s3/)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[UCloud](https://www.ucloud.cn)|[UFile](https://www.ucloud.cn/site/product/ufile.html)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[华为云](https://www.huaweicloud.com)|[OBS](https://www.huaweicloud.com/product/obs.html)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[京东云](https://www.jdcloud.com)|[对象存储](https://www.jdcloud.com/products/cloudstorag)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[青云](https://www.qingcloud.com)|[对象存储](https://www.qingcloud.com/products/qingstor/)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[金山云](https://www.ksyun.com)|[对象存储](https://www.ksyun.com/post/product/KS3)|![不支持](https://img.shields.io/badge/support-No-red.svg)|仅限企业用户|
|[Azure](https://www.azure.cn/zh-cn/)|[Blob 存储](https://www.azure.cn/zh-cn/home/features/storage/blobs/)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||
|[Google Cloud](https://cloud.google.com)|[Google Cloud Storage](https://cloud.google.com/storage/)|![支持](https://img.shields.io/badge/support-Yes-green.svg)||  
|自定义|FTP|![支持](https://img.shields.io/badge/support-Yes-green.svg)||  

# 支持的数据源

|数据源|是否支持|
|-|-|
|本地文件|![支持](https://img.shields.io/badge/support-Yes-green.svg)|
|本地文件夹|![支持](https://img.shields.io/badge/support-Yes-green.svg)|
|MySQL|![即将到来](https://img.shields.io/badge/support-Future-yellow.svg)|
|SQL Server|![即将到来](https://img.shields.io/badge/support-Future-yellow.svg)|
|Oracle|![即将到来](https://img.shields.io/badge/support-Future-yellow.svg)|
|自定义命令|![支持](https://img.shields.io/badge/support-Yes-green.svg)|
|FTP文件|![支持](https://img.shields.io/badge/support-Yes-green.svg)|
|FTP文件文件夹|![支持](https://img.shields.io/badge/support-Yes-green.svg)|

# 支持的备份触发方式

|触发方式|是否支持|
|-|-|
|Cron 表达式|![支持](https://img.shields.io/badge/support-Yes-green.svg)|
|本地文件发生变化|![即将到来](https://img.shields.io/badge/support-Future-yellow.svg)|
|本地文件夹发生变化|![即将到来](https://img.shields.io/badge/support-Future-yellow.svg)|

# 使用方法(Docker)
### 运行
```
docker run -d --restart=always -v /etc/localtime:/etc/localtime:ro -v /xxx/yyy:/data -v /etc/backup2cloud:/conf sanjusss/backup2cloud run -c /conf/backup2cloud.json
```
运行后程序将加载配置文件backup2cloud.json。  
`-v /etc/localtime:/etc/localtime:ro`保证了docker容器内时区和外部环境一样，否则默认使用UTC时间。  
注意文件夹的挂载。  

### 查看示例配置文件
```
docker run --rm -v /D/share:/conf sanjusss/backup2cloud eg -s /conf/eg.json
```
运行后将在D:\share\eg.json生成示例配置文件。  
如果省略`-s /conf/eg.json`，将不保存文件，仅在命令行下输出。  
实际的配置文件中没有Tips一项。  
其他配置参数见[查看示例](https://github.com/sanjusss/backup2cloud#查看示例)。

# 使用方法(Windows)
### 下载
[点我打开软件下载页面](https://github.com/sanjusss/backup2cloud/releases/latest)  

### 运行
```
backup2cloud run -c D:\backup2cloud.json
```
运行后程序将加载配置文件backup2cloud.json。

### 查看示例
1. 查看一个示例配置文件
```
backup2cloud eg -s D:\share\eg.json
```
运行后将在D:\share\eg.json生成示例配置文件。  
如果省略`-s D:\share\eg.json`，将不保存文件，仅在命令行下输出。  
实际的配置文件中没有Tips一项。  

2. 查看所有上传类名称
```
backup2cloud eg -l uploader
```

3. 查看所有数据源类名称
```
backup2cloud eg -l datasource
```

4. 查看指定上传类示例
```
backup2cloud eg -u 上传类名称
```

5. 查看指定数据源类示例
```
backup2cloud eg -d 数据源类名称
```


# 配置文件说明
待补充

# 引用项目
https://www.newtonsoft.com/json  
https://github.com/vla/aliyun-oss-csharp-sdk  
https://github.com/commandlineparser/commandline  
https://github.com/zhengchun/qcloud-sdk-net  
https://github.com/adamhathcock/sharpcompress  
https://www.quartz-scheduler.net  
https://github.com/sanjusss/backup2cloud  
https://github.com/fengyhack/csharp-sdk  
https://github.com/sanjusss/ucloud-csharp-sdk  
https://github.com/aws/aws-sdk-net/  
https://github.com/awslabs/aws-sdk-net-samples  
https://support.huaweicloud.com/devg-obs_csharp_sdk_doc_zh/zh-cn_topic_0120606335.html  
https://github.com/GoogleCloudPlatform/google-cloud-dotnet  
https://github.com/Azure/azure-storage-net  
